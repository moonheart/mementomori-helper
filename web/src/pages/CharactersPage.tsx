import { useEffect, useState, useMemo } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Search, Swords, Zap, BookOpen, TrendingUp, Star, ArrowUp, Shield, Users, Loader2 } from 'lucide-react';
import { LevelLinkPanel } from '@/components/character/LevelLinkPanel';
import { useAccountStore } from '@/store/accountStore';
import { ortegaApi } from '@/api/ortega-client';
import { useMasterStore } from '@/store/masterStore';
import { useLocalizationStore } from '@/store/localization-store';
import { UserCharacterDtoInfo, CharacterRarityFlags, ElementType, JobFlags, CharacterMB } from '@/api/generated';
import { CharacterDetailDialog } from '@/components/characters/CharacterDetailDialog';
import type { UICharacter } from '@/components/characters/types';
import { CharacterIcon } from '@/components/character/CharacterIcon';
import type { UserSyncData } from '@/api/generated/userSyncData';

export function CharactersPage() {
    const { currentAccountId } = useAccountStore();
    const t = useLocalizationStore(state => state.t);
    const getTable = useMasterStore(state => state.getTable);

    const [loading, setLoading] = useState(false);
    const [userCharacters, setUserCharacters] = useState<UserCharacterDtoInfo[]>([]);
    const [characterMasterMap, setCharacterMasterMap] = useState<Record<number, CharacterMB>>({});
    const [userSyncData, setUserSyncData] = useState<UserSyncData | null>(null);

    const [search, setSearch] = useState('');
    const [selectedRarity, setSelectedRarity] = useState<string>('all');
    const [selectedElement, setSelectedElement] = useState<string>('all');
    const [selectedClass, setSelectedClass] = useState<string>('all');
    const [selectedCharacter, setSelectedCharacter] = useState<UICharacter | null>(null);
    const [detailDialogOpen, setDetailDialogOpen] = useState(false);

    // 获取数据
    useEffect(() => {
        if (!currentAccountId) return;

        const fetchData = async () => {
            setLoading(true);
            try {
                // 1. 获取用户角色
                const userData = await ortegaApi.user.getUserData({});
                const syncData = userData.userSyncData ?? null;
                const chars = syncData?.userCharacterDtoInfos || [];
                setUserCharacters(chars);
                setUserSyncData(syncData);

                // 2. 获取 Master 数据
                const masterTable = await getTable<CharacterMB>('CharacterTable');
                const masterMap: Record<number, CharacterMB> = {};
                masterTable.forEach((m) => {
                    masterMap[m.id] = m;
                });
                setCharacterMasterMap(masterMap);
            } catch (error) {
                console.error('Failed to fetch character data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [currentAccountId, getTable]);

    // 合并数据并翻译
    const characters: UICharacter[] = useMemo(() => {
        return userCharacters.map(char => {
            const master = characterMasterMap[char.characterId];
            return {
                ...char,
                master,
                nameKey: master?.nameKey ?? t('CHARACTER_FALLBACK_NAME', [char.characterId]),
                element: master?.elementType ?? ElementType.None,
                job: master?.jobFlags ?? JobFlags.None
            };
        });
    }, [userCharacters, characterMasterMap, t]);

    // 过滤和排序逻辑
    const filteredCharacters = useMemo(() => {
        const filtered = characters.filter(char => {
            const resolvedName = char.nameKey.startsWith('[') ? t(char.nameKey) : char.nameKey;
            const matchesSearch = resolvedName.toLowerCase().includes(search.toLowerCase());

            let matchesRarity = true;
            if (selectedRarity !== 'all') {
                const rarityValue = parseInt(selectedRarity);
                matchesRarity = char.rarityFlags === rarityValue;
            }

            let matchesElement = true;
            if (selectedElement !== 'all') {
                const elementValue = parseInt(selectedElement);
                matchesElement = char.element === elementValue;
            }

            let matchesClass = true;
            if (selectedClass !== 'all') {
                const classValue = parseInt(selectedClass);
                // JobFlags 是位标志，但单角色通常只有一个职业
                matchesClass = (char.job & classValue) !== 0;
            }

            return matchesSearch && matchesRarity && matchesElement && matchesClass;
        });

        // 排序方式: 等级优先 + 稀有度优先
        return [...filtered].sort((a, b) => {
            // 1. 等级降序
            if (b.level !== a.level) {
                return b.level - a.level;
            }
            // 2. 稀有度降序
            return (b.rarityFlags || 0) - (a.rarityFlags || 0);
        });
    }, [characters, search, selectedRarity, selectedElement, selectedClass, t]);

    // 统计数据
    const stats = useMemo(() => {
        if (characters.length === 0) return { total: 0, highRarityCount: 0, avgLevel: 0, maxLevel: 0 };
        return {
            total: characters.length,
            highRarityCount: characters.filter(c => c.rarityFlags >= CharacterRarityFlags.SSR).length,
            avgLevel: Math.round(characters.reduce((sum, c) => sum + c.level, 0) / characters.length),
            maxLevel: Math.max(...characters.map(c => c.level))
        };
    }, [characters]);

    const getElementData = (element: ElementType) => {
        const data: Record<number, { nameKey: string, color: string, bg: string, icon: string }> = {
            [ElementType.Red]: { nameKey: '[ElementTypeRed]', color: 'text-red-500', bg: 'bg-red-500/10', icon: '🔥' },
            [ElementType.Blue]: { nameKey: '[ElementTypeBlue]', color: 'text-blue-500', bg: 'bg-blue-500/10', icon: '💧' },
            [ElementType.Green]: { nameKey: '[ElementTypeGreen]', color: 'text-green-500', bg: 'bg-green-500/10', icon: '🍃' },
            [ElementType.Yellow]: { nameKey: '[ElementTypeYellow]', color: 'text-yellow-600', bg: 'bg-yellow-500/10', icon: '⚡' },
            [ElementType.Light]: { nameKey: '[ElementTypeLight]', color: 'text-yellow-400', bg: 'bg-yellow-400/10', icon: '☀️' },
            [ElementType.Dark]: { nameKey: '[ElementTypeDark]', color: 'text-purple-500', bg: 'bg-purple-500/10', icon: '🌙' }
        };
        return data[element] || { nameKey: '[CommonNoneLabel]', color: 'text-gray-500', bg: 'bg-gray-500/10', icon: '❓' };
    };

    const getJobData = (job: JobFlags) => {
        if (job & JobFlags.Warrior) return { nameKey: '[JobFlagsWarrior]', color: 'text-red-600', icon: Swords, descKey: '[JobDescriptionWarrior]' };
        if (job & JobFlags.Sniper) return { nameKey: '[JobFlagsSniper]', color: 'text-green-600', icon: Zap, descKey: '[JobDescriptionSniper]' };
        if (job & JobFlags.Sorcerer) return { nameKey: '[JobFlagsSorcerer]', color: 'text-purple-600', icon: BookOpen, descKey: '[JobDescriptionSorcerer]' };
        return { nameKey: '[PictureBookRefineDialogJobFlags]', color: 'text-gray-600', icon: Swords, descKey: '' };
    };

    const getRarityData = (rarity: CharacterRarityFlags) => {
        const data: Partial<Record<CharacterRarityFlags, { name: string, color: string, text: string, max: number }>> = {
            [CharacterRarityFlags.N]: { name: 'N', color: 'bg-gray-400', text: 'text-gray-700', max: 20 },
            [CharacterRarityFlags.R]: { name: 'R', color: 'bg-green-500', text: 'text-green-700', max: 40 },
            [CharacterRarityFlags.RPlus]: { name: 'R+', color: 'bg-green-600', text: 'text-green-700', max: 60 },
            [CharacterRarityFlags.SR]: { name: 'SR', color: 'bg-blue-500', text: 'text-blue-700', max: 80 },
            [CharacterRarityFlags.SRPlus]: { name: 'SR+', color: 'bg-blue-600', text: 'text-blue-700', max: 100 },
            [CharacterRarityFlags.SSR]: { name: 'SSR', color: 'bg-purple-500', text: 'text-purple-700', max: 120 },
            [CharacterRarityFlags.SSRPlus]: { name: 'SSR+', color: 'bg-purple-600', text: 'text-purple-700', max: 140 },
            [CharacterRarityFlags.UR]: { name: 'UR', color: 'bg-yellow-500', text: 'text-yellow-700', max: 160 },
            [CharacterRarityFlags.URPlus]: { name: 'UR+', color: 'bg-yellow-600', text: 'text-yellow-700', max: 180 },
            [CharacterRarityFlags.LR]: { name: 'LR', color: 'bg-orange-500', text: 'text-orange-700', max: 240 }
        };
        // 简单处理 LR+
        if (rarity >= CharacterRarityFlags.LR) {
            return data[CharacterRarityFlags.LR]!;
        }
        return data[rarity] || { name: '?', color: 'bg-gray-500', text: 'text-gray-700', max: 1 };
    };

    if (!currentAccountId) {
        return (
            <div className="flex items-center justify-center h-[60vh]">
                <Card className="w-full max-w-md">
                    <CardHeader>
                        <CardTitle>{t('CHARACTER_NOT_LOGGED_IN_TITLE')}</CardTitle>
                        <CardDescription>{t('CHARACTER_NOT_LOGGED_IN_DESC')}</CardDescription>
                    </CardHeader>
                </Card>
            </div>
        );
    }

    if (loading && characters.length === 0) {
        return (
            <div className="flex items-center justify-center h-[60vh]">
                <div className="text-center">
                    <Loader2 className="h-8 w-8 animate-spin mx-auto mb-4" />
                    <p className="text-muted-foreground">{t('CHARACTER_LOADING')}</p>
                </div>
            </div>
        );
    }

    return (
        <div className="space-y-6">
            {/* Summary Stats */}
            <div className="grid gap-4 md:grid-cols-4">
                <Card>
                    <CardContent className="p-6">
                        <div className="flex items-center justify-between">
                            <div>
                                <p className="text-sm text-muted-foreground">{t('[CharacterMenuTabOwnedCharacter]')}</p>
                                <p className="text-2xl font-bold">{stats.total}</p>
                            </div>
                            <Users className="h-8 w-8 text-blue-500" />
                        </div>
                    </CardContent>
                </Card>
                <Card>
                    <CardContent className="p-6">
                        <div className="flex items-center justify-between">
                            <div>
                                <p className="text-sm text-muted-foreground">{t('CHARACTER_SSR_AND_ABOVE')}</p>
                                <p className="text-2xl font-bold">{stats.highRarityCount}</p>
                            </div>
                            <Star className="h-8 w-8 text-yellow-500" />
                        </div>
                    </CardContent>
                </Card>
                <Card>
                    <CardContent className="p-6">
                        <div className="flex items-center justify-between">
                            <div>
                                <p className="text-sm text-muted-foreground">{t('CHARACTER_AVG_LEVEL')}</p>
                                <p className="text-2xl font-bold">{stats.avgLevel}</p>
                            </div>
                            <ArrowUp className="h-8 w-8 text-green-500" />
                        </div>
                    </CardContent>
                </Card>
                <Card>
                    <CardContent className="p-6">
                        <div className="flex items-center justify-between">
                            <div>
                                <p className="text-sm text-muted-foreground">{t('[LevelLinkMaxLevel]')}</p>
                                <p className="text-2xl font-bold">{stats.maxLevel}</p>
                            </div>
                            <TrendingUp className="h-8 w-8 text-orange-500" />
                        </div>
                    </CardContent>
                </Card>
            </div>

            <LevelLinkPanel />

            {/* Filters */}
            <Card>
                <CardContent className="p-6">
                    <div className="grid gap-4 md:grid-cols-4">
                        <div className="relative">
                            <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                            <Input
                                placeholder={t('CHARACTER_SEARCH_PLACEHOLDER')}
                                value={search}
                                onChange={(e) => setSearch(e.target.value)}
                                className="pl-10"
                            />
                        </div>

                        <Select value={selectedRarity} onValueChange={setSelectedRarity}>
                            <SelectTrigger>
                                <SelectValue placeholder={t('[CharacterSortTypeRarity]')} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="all">{t('CHARACTER_ALL_RARITIES')}</SelectItem>
                                <SelectItem value={CharacterRarityFlags.LR.toString()}>LR</SelectItem>
                                <SelectItem value={CharacterRarityFlags.UR.toString()}>UR</SelectItem>
                                <SelectItem value={CharacterRarityFlags.SSR.toString()}>SSR</SelectItem>
                                <SelectItem value={CharacterRarityFlags.SR.toString()}>SR</SelectItem>
                                <SelectItem value={CharacterRarityFlags.R.toString()}>R</SelectItem>
                            </SelectContent>
                        </Select>

                        <Select value={selectedElement} onValueChange={setSelectedElement}>
                            <SelectTrigger>
                                <SelectValue placeholder={t('[CharacterSortTypeCharacterAttribute]')} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="all">{t('CHARACTER_ALL_ELEMENTS')}</SelectItem>
                                <SelectItem value={ElementType.Red.toString()}>🔥 {t('[ElementTypeRed]')}</SelectItem>
                                <SelectItem value={ElementType.Blue.toString()}>💧 {t('[ElementTypeBlue]')}</SelectItem>
                                <SelectItem value={ElementType.Green.toString()}>🍃 {t('[ElementTypeGreen]')}</SelectItem>
                                <SelectItem value={ElementType.Yellow.toString()}>⚡ {t('[ElementTypeYellow]')}</SelectItem>
                                <SelectItem value={ElementType.Light.toString()}>☀️ {t('[ElementTypeLight]')}</SelectItem>
                                <SelectItem value={ElementType.Dark.toString()}>🌙 {t('[ElementTypeDark]')}</SelectItem>
                            </SelectContent>
                        </Select>

                        <Select value={selectedClass} onValueChange={setSelectedClass}>
                            <SelectTrigger>
                                <SelectValue placeholder={t('CHARACTER_JOB_PLACEHOLDER')} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="all">{t('CHARACTER_ALL_JOBS')}</SelectItem>
                                <SelectItem value={JobFlags.Warrior.toString()}>⚔️ {t('[JobFlagsWarrior]')}</SelectItem>
                                <SelectItem value={JobFlags.Sniper.toString()}>🏹 {t('[JobFlagsSniper]')}</SelectItem>
                                <SelectItem value={JobFlags.Sorcerer.toString()}>📖 {t('[JobFlagsSorcerer]')}</SelectItem>
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="mt-4">
                        <p className="text-sm text-muted-foreground">
                            {t('CHARACTER_FOUND_COUNT', [filteredCharacters.length])}
                        </p>
                    </div>
                </CardContent>
            </Card>

            {/* Characters Grid */}
            <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
                {filteredCharacters.map((character) => {
                    const elementData = getElementData(character.element);
                    const jobData = getJobData(character.job);
                    const rarityData = getRarityData(character.rarityFlags);
                    const JobIcon = jobData.icon;

                    const handleCardClick = () => {
                        setSelectedCharacter(character);
                        setDetailDialogOpen(true);
                    };

                    return (
                        <Card key={character.guid} className="overflow-hidden hover:shadow-lg transition-all duration-300 cursor-pointer group border-0 shadow-sm" onClick={handleCardClick}>
                            <div className="flex">
                                {/* 左侧方形头像区域 */}
                                <div className="relative w-24 h-24 shrink-0 flex items-center justify-center">
                                    <CharacterIcon
                                        size={96}
                                        userSyncData={userSyncData}
                                        userCharacterInfo={{
                                            guid: character.guid,
                                            playerId: character.playerId,
                                            characterId: character.characterId,
                                            level: character.level,
                                            subLevel: 0,
                                            exp: character.exp,
                                            rarityFlags: character.rarityFlags,
                                            isLocked: character.isLocked,
                                        }}
                                    />
                                    {/* 锁定图标 */}
                                    {character.isLocked && (
                                        <div className="absolute top-1 right-1 bg-black/60 rounded-full p-0.5">
                                            <Shield className="h-2.5 w-2.5 text-white" />
                                        </div>
                                    )}
                                </div>

                                {/* 右侧信息区域 */}
                                <div className="flex-1 p-2.5 min-w-0 flex flex-col justify-between">
                                    {/* 顶部：名称和稀有度 */}
                                    <div>
                                        <div className="flex items-center gap-1.5 mb-1">
                                            <h3 className="font-bold text-sm text-gray-900 truncate">
                                                {character.nameKey.startsWith('[') ? t(character.nameKey) : character.nameKey}
                                            </h3>
                                            <span className={`text-[10px] px-1 py-0 rounded ${rarityData.color} text-white`}>
                                                {rarityData.name}
                                            </span>
                                        </div>
                                        {/* 职业和属性 */}
                                        <div className="flex items-center gap-2 text-xs text-gray-500">
                                            <span className="flex items-center gap-0.5">
                                                <JobIcon className={`h-3 w-3 ${jobData.color}`} />
                                                {jobData.nameKey.startsWith('[') ? t(jobData.nameKey) : jobData.nameKey}
                                            </span>
                                            <span className="flex items-center gap-0.5">
                                                <span>{elementData.icon}</span>
                                                {elementData.nameKey.startsWith('[') ? t(elementData.nameKey) : elementData.nameKey}
                                            </span>
                                        </div>
                                    </div>

                                    {/* 底部：等级和进度 */}
                                    <div className="space-y-1">
                                        <div className="flex items-center justify-between text-[10px]">
                                            <span className="text-gray-400">{t('[CommonPlayerRankLabel]')}</span>
                                            <span className="font-medium text-gray-700">Lv.{character.level}/{rarityData.max}</span>
                                        </div>
                                        <div className="h-1 bg-gray-100 rounded-full overflow-hidden">
                                            <div
                                                className={`h-full ${rarityData.color}`}
                                                style={{ width: `${Math.min((character.level / rarityData.max) * 100, 100)}%` }}
                                            />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </Card>
                    );
                })}
            </div>

            {filteredCharacters.length === 0 && !loading && (
                <Card>
                    <CardContent className="text-center py-12">
                        <p className="text-muted-foreground">{t('CHARACTER_EMPTY_TITLE')}</p>
                        <p className="text-sm text-muted-foreground mt-2">{t('CHARACTER_EMPTY_DESC')}</p>
                    </CardContent>
                </Card>
            )}

            <CharacterDetailDialog
                character={selectedCharacter}
                open={detailDialogOpen}
                onOpenChange={setDetailDialogOpen}
            />
        </div>
    );
}
