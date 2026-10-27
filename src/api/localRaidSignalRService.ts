import * as signalR from '@microsoft/signalr';
import { LocalRaidPartyInfo } from '@/api/generated';
import { useAccountStore } from '@/store/accountStore';

// 重新导出类型
export type { LocalRaidPartyInfo } from '@/api/generated';

/**
 * LocalRaid 会话状态
 */
export interface LocalRaidSessionState {
    isConnected: boolean;
    isInRoom: boolean;
    currentRoom: LocalRaidPartyInfo | null;
}

/**
 * LocalRaid 会话信息
 */
export interface LocalRaidSessionInfo {
    userId: number;
    playerId: number;
    playerName: string;
    isConnected: boolean;
}

/**
 * LocalRaid 错误信息
 */
export interface LocalRaidError {
    errorCode: number;
    message: string;
}

/**
 * 邀请信息
 */
export interface LocalRaidInviteInfo {
    invitePlayerId: number;
    invitePlayerName: string;
    questId: number;
    roomId: string;
    battlePower: number;
    playerRank: number;
    characterIconId: number;
}

/**
 * 创建房间请求参数
 */
export interface CreateRoomParams {
    conditionsType: number;
    questId: number;
    requiredBattlePower: number;
    password: number;
    isAutoStart: boolean;
}

/**
 * 加入房间请求参数
 */
export interface JoinRoomParams {
    roomId: string;
    password: number;
}

/**
 * 事件回调接口
 */
export interface LocalRaidCallbacks {
    onSessionJoined?: (info: LocalRaidSessionInfo) => void;
    onSessionLeft?: () => void;
    onRoomListReceived?: (rooms: LocalRaidPartyInfo[]) => void;
    onRoomJoined?: (room: LocalRaidPartyInfo) => void;
    onRoomUpdated?: (room: LocalRaidPartyInfo) => void;
    onRoomLeft?: () => void;
    onRoomDisbanded?: () => void;
    onRoomLocked?: () => void;
    onBattleStarted?: () => void;
    onReadyBattle?: () => void;
    onInvited?: (invite: LocalRaidInviteInfo) => void;
    onInviteRefused?: (response: unknown) => void;
    onRefused?: () => void;
    onError?: (error: LocalRaidError) => void;
    onPartyCountUpdated?: (counts: Record<number, number>) => void;
    onConnectionStateChanged?: (connected: boolean) => void;
}

/**
 * LocalRaid SignalR 服务
 * 处理 Web 前端与后端的实时通信
 */
class LocalRaidSignalRService {
    private connection: signalR.HubConnection | null = null;
    private readonly hubUrl: string;
    private callbacks: LocalRaidCallbacks = {};
    private isConnected = false;
    private reconnectAttempts = 0;
    private maxReconnectAttempts = 5;

    constructor() {
        const baseUrl = import.meta.env.VITE_API_URL ?? 'http://localhost:5000';
        this.hubUrl = `${baseUrl}/hubs/localraid`;
    }

    /**
     * 设置事件回调
     */
    setCallbacks(callbacks: LocalRaidCallbacks) {
        this.callbacks = { ...this.callbacks, ...callbacks };
    }

    /**
     * 启动连接
     */
    async start(): Promise<void> {
        if (this.connection && this.isConnected) {
            return;
        }

        // 获取当前用户 ID
        const currentAccountId = useAccountStore.getState().currentAccountId;
        if (!currentAccountId) {
            throw new Error('No account selected');
        }

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.hubUrl}?userId=${currentAccountId}`, {
                withCredentials: true
            })
            .withAutomaticReconnect({
                nextRetryDelayInMilliseconds: retryContext => {
                    if (retryContext.previousRetryCount >= this.maxReconnectAttempts) {
                        return null;
                    }
                    return Math.min(1000 * Math.pow(2, retryContext.previousRetryCount), 30000);
                }
            })
            .build();

        this.setupHandlers();

        try {
            await this.connection.start();
            
            // 等待连接就绪
            let retries = 10;
            while (this.connection.state !== signalR.HubConnectionState.Connected && retries-- > 0) {
                await new Promise(resolve => setTimeout(resolve, 100));
            }
            
            if (this.connection.state !== signalR.HubConnectionState.Connected) {
                throw new Error('Connection failed to establish');
            }
            
            this.isConnected = true;
            this.reconnectAttempts = 0;
            this.callbacks.onConnectionStateChanged?.(true);
            console.log('LocalRaid SignalR Connected');
        } catch (err) {
            console.error('LocalRaid SignalR Connection Error:', err);
            throw err;
        }
    }

    /**
     * 停止连接
     */
    async stop(): Promise<void> {
        if (this.connection) {
            await this.connection.stop();
            this.connection = null;
            this.isConnected = false;
            this.callbacks.onConnectionStateChanged?.(false);
        }
    }

    /**
     * 设置 SignalR 事件处理器
     */
    private setupHandlers(): void {
        if (!this.connection) return;

        // 连接事件
        this.connection.onclose(() => {
            this.isConnected = false;
            this.callbacks.onConnectionStateChanged?.(false);
            console.log('LocalRaid SignalR Disconnected');
        });

        this.connection.onreconnected(() => {
            this.isConnected = true;
            this.reconnectAttempts = 0;
            this.callbacks.onConnectionStateChanged?.(true);
            console.log('LocalRaid SignalR Reconnected');
        });

        // 房间列表
        this.connection.on('OnRoomListReceived', (rooms: LocalRaidPartyInfo[]) => {
            this.callbacks.onRoomListReceived?.(rooms);
        });

        // 加入房间
        this.connection.on('OnRoomJoined', (room: LocalRaidPartyInfo) => {
            this.callbacks.onRoomJoined?.(room);
        });

        // 房间更新
        this.connection.on('OnRoomUpdated', (room: LocalRaidPartyInfo) => {
            this.callbacks.onRoomUpdated?.(room);
        });

        // 离开房间
        this.connection.on('OnLeaveRoom', () => {
            this.callbacks.onRoomLeft?.();
        });

        // 房间解散
        this.connection.on('OnRoomDisbanded', () => {
            this.callbacks.onRoomDisbanded?.();
        });

        // 房间锁定
        this.connection.on('OnRoomLocked', () => {
            this.callbacks.onRoomLocked?.();
        });

        // 战斗开始
        this.connection.on('OnBattleStarted', () => {
            this.callbacks.onBattleStarted?.();
        });

        // 准备战斗
        this.connection.on('OnReadyBattle', () => {
            this.callbacks.onReadyBattle?.();
        });

        // 被邀请
        this.connection.on('OnInvited', (invite: LocalRaidInviteInfo) => {
            this.callbacks.onInvited?.(invite);
        });

        // 邀请被拒绝
        this.connection.on('OnInviteRefused', (response: unknown) => {
            this.callbacks.onInviteRefused?.(response);
        });

        // 被拒绝
        this.connection.on('OnRefused', () => {
            this.callbacks.onRefused?.();
        });

        // 错误
        this.connection.on('OnError', (error: LocalRaidError) => {
            this.callbacks.onError?.(error);
        });

        // 队伍人数更新
        this.connection.on('OnPartyCountUpdated', (counts: Record<number, number>) => {
            this.callbacks.onPartyCountUpdated?.(counts);
        });
    }

    // ==================== Hub 方法调用 ====================

    /**
     * 加入 LocalRaid 会话
     */
    async joinSession(): Promise<LocalRaidSessionInfo> {
        await this.ensureConnection();
        return await this.connection!.invoke('JoinSession');
    }

    /**
     * 离开 LocalRaid 会话
     */
    async leaveSession(): Promise<void> {
        await this.connection?.invoke('LeaveSession');
    }

    /**
     * 获取当前会话状态
     */
    async getCurrentState(): Promise<LocalRaidSessionState> {
        await this.ensureConnection();
        return await this.connection!.invoke('GetCurrentState');
    }

    /**
     * 获取房间列表
     */
    async getRoomList(questId: number): Promise<void> {
        await this.ensureConnection();
        await this.connection!.invoke('GetRoomList', questId);
    }

    /**
     * 创建房间
     */
    async createRoom(params: CreateRoomParams): Promise<void> {
        await this.ensureConnection();
        await this.connection!.invoke('CreateRoom', params);
    }

    /**
     * 加入房间
     */
    async joinRoom(params: JoinRoomParams): Promise<void> {
        await this.ensureConnection();
        await this.connection!.invoke('JoinRoom', params);
    }

    /**
     * 随机加入房间
     */
    async joinRandomRoom(questId: number): Promise<void> {
        await this.ensureConnection();
        await this.connection!.invoke('JoinRandomRoom', questId);
    }

    /**
     * 设置准备状态
     */
    async setReady(isReady: boolean): Promise<void> {
        await this.ensureConnection();
        await this.connection!.invoke('SetReady', isReady);
    }

    /**
     * 开始战斗
     */
    async startBattle(): Promise<void> {
        await this.ensureConnection();
        await this.connection!.invoke('StartBattle');
    }

    /**
     * 离开房间
     */
    async leaveRoom(): Promise<void> {
        console.log('离开房间');
        await this.ensureConnection();
        await this.connection!.invoke('LeaveRoom');
    }

    /**
     * 更新房间条件
     */
    async updateRoomCondition(params: {
        conditionsType: number;
        requiredBattlePower: number;
        password: number;
    }): Promise<void> {
        await this.ensureConnection();
        await this.connection!.invoke('UpdateRoomCondition', params);
    }

    /**
     * 邀请好友
     */
    async inviteFriend(friendPlayerId: number): Promise<void> {
        await this.ensureConnection();
        await this.connection!.invoke('InviteFriend', friendPlayerId);
    }

    /**
     * 解散房间
     */
    async disbandRoom(): Promise<void> {
        console.log('解散房间');
        await this.ensureConnection();
        await this.connection!.invoke('DisbandRoom');
    }

    /**
     * 确保连接已建立
     */
    private async ensureConnection(): Promise<void> {
        if (!this.connection || !this.isConnected) {
            await this.start();
        }
    }
}

export const localRaidSignalRService = new LocalRaidSignalRService();
