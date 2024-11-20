import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
	name: "filter",
})
export class GoalFilterPipe implements PipeTransform {
	transform(items: any[], filterValue: string, filterKey: string): any[] {
		if (!items) return [];
		if (!filterValue) return items;

		return items.filter((item) => item[filterKey] === filterValue);
	}
}
