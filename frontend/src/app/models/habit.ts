export interface Habit {
	id?: number;
	name: string;
	description: string;
	currentStreak: number;
	bestStreak: number;
	lastCheckIn: string;
	isActive: boolean;
}
