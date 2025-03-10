import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ExportService } from "../services/export.service";
import { ExportData } from "../models/export.model";

@Component({
	selector: "app-export",
	templateUrl: "./export.component.html",
})
export class ExportComponent implements OnInit {
	exportForm: FormGroup;
	exportHistory: ExportData[] = [];

	constructor(
		private fb: FormBuilder,
		private exportService: ExportService,
	) {
		this.exportForm = this.fb.group({
			name: ["My Export", Validators.required],
			description: ["Exported mood data", Validators.required],
		});
	}

	ngOnInit(): void {
		this.loadExportHistory();
	}

	loadExportHistory(): void {
		this.exportService.getExportHistory().subscribe(
			(data) => {
				this.exportHistory = data;
			},
			(error) => {
				console.error("Error loading export history:", error);
			},
		);
	}

	exportAs(format: string): void {
		if (this.exportForm.valid) {
			const exportRequest = {
				...this.exportForm.value,
				format: format,
			};

			this.exportService.exportData(exportRequest).subscribe(
				(data) => {
					this.downloadFile(data, format, exportRequest.name);
					this.loadExportHistory(); // Refresh history after export
				},
				(error) => {
					console.error("Export error:", error);
				},
			);
		}
	}

	downloadFile(data: Blob, format: string, fileName: string): void {
		const blob = new Blob([data], { type: this.getContentType(format) });
		const url = window.URL.createObjectURL(blob);
		const link = document.createElement("a");
		link.href = url;
		link.download = `${fileName}.${format.toLowerCase()}`;
		link.click();
		window.URL.revokeObjectURL(url);
	}

	getContentType(format: string): string {
		switch (format.toLowerCase()) {
			case "json":
				return "application/json";
			case "csv":
				return "text/csv";
			case "pdf":
				return "application/pdf";
			default:
				return "application/octet-stream";
		}
	}
}
