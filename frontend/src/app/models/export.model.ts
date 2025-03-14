export interface ExportData {
    id?: number;
    name: string;
    description: string;
    format: string;
    date?: Date;
    userId?: number;
}

export interface ExportRequest {
    name: string;
    description: string;
    format: string;
}
