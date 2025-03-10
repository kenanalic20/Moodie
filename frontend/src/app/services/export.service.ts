import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ExportData, ExportRequest } from "../models/export.model";

@Injectable({
	providedIn: "root",
})
export class ExportService {
	private apiUrl = "https://localhost:8001/api/export";

	constructor(private http: HttpClient) {}

	exportData(exportRequest: ExportRequest): Observable<Blob> {
		return this.http.post(this.apiUrl, exportRequest, {
			responseType: "blob",
			withCredentials: true,
		});
	}

	getExportHistory(): Observable<ExportData[]> {
		return this.http.get<ExportData[]>(this.apiUrl, { withCredentials: true });
	}
}
