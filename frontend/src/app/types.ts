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
	activity?: Activity;
	userId?: number;
}

export interface Activity {
	id: number;
	name: string;
	description?: string;
}
