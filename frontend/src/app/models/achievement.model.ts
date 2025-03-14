export interface Achievement {
    id: number;
    name: string;
    description: string;
    badgeImage: string;
    pointValue: number;
    criteria: string;
    slug: string;
}

export interface UserAchievement {
    id: number;
    dateEarned: string;
    userId: number;
    achievementId: number;
    achievement: Achievement;
}
