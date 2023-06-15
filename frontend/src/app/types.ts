export type Mood = {
    mood: number;
    date: Date;
    note: string;
    image: string;
};

export type Day = {
    dayOfMonth: number;
    dayName: string;
    isLast: boolean;
    mood?: Mood;
};
