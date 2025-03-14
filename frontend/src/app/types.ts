export interface Day {
    dayOfMonth: number;
    dayName: string;
    isLast: boolean;
    mood?: Mood; // keep for backwards compatibility
    moods?: Mood[];
}

export interface Mood {
    id?: number;
    moodValue: number;
    date: Date;
    note?: string;
    image?: string;
    activityId?: number;
    moodActivities?: Activity[];
    userId?: number;
    notes?: {
        id: number;
        title: string;
        description: string;
        image: string | null;
    }[];
}

export interface Activity {
    id: number;
    name: string;
    description?: string;
}
