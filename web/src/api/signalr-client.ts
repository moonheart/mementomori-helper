import * as signalR from '@microsoft/signalr';
import { API_BASE } from '@/lib/base-path';

class SignalRService {
    private connection: signalR.HubConnection | null = null;
    private readonly hubUrl: string;

    constructor() {
        this.hubUrl = `${API_BASE}/hubs/game`;
    }

    async start() {
        if (this.connection) {
            return;
        }

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(this.hubUrl)
            .withAutomaticReconnect()
            .build();

        this.setupHandlers();

        try {
            await this.connection.start();
            console.log('SignalR Connected');
        } catch (err) {
            console.error('SignalR Connection Error:', err);
            setTimeout(() => this.start(), 5000);
        }
    }

    async stop() {
        if (this.connection) {
            await this.connection.stop();
            this.connection = null;
        }
    }

    private setupHandlers() {
        if (!this.connection) return;

        this.connection.on('Subscribed', (accountId: number) => {
            console.log(`Subscribed to account: ${accountId}`);
        });

        // Add more handlers as needed
        // this.connection.on('ReceiveTaskUpdate', (message) => { ... });
    }

    async subscribeToAccount(accountId: number) {
        if (!this.connection) {
            await this.start();
        }
        await this.connection?.invoke('SubscribeToAccount', accountId);
    }

    async unsubscribeFromAccount(accountId: number) {
        await this.connection?.invoke('UnsubscribeFromAccount', accountId);
    }
}

/**
 * 创建 JobHub 连接用于接收后台任务日志
 */
export function createJobHubConnection(userId: number, onReceiveLog: (message: string) => void) {
    const hubUrl = `${API_BASE}/hubs/jobs?userId=${userId}`;

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .withAutomaticReconnect()
        .build();

    connection.on('ReceiveLog', (data: { time: string; level: string; message: string }) => {
        onReceiveLog(data.message);
    });

    return connection;
}

export const signalRService = new SignalRService();
