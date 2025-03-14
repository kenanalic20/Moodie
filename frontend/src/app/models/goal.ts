export interface Goal {
    id?: number;
    name: string;
    description: string;
    goalType: string;
    startDate: string;
    endDate: string;
    completed?: boolean;
}
