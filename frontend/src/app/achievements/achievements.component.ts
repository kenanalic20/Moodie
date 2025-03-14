import { Component, OnInit } from '@angular/core';
import { AchievementsService } from '../services/achievements.service';
import { UserAchievement } from '../models/achievement.model';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-achievements',
    templateUrl: './achievements.component.html',
})
export class AchievementsComponent implements OnInit {
    userAchievements: UserAchievement[] = [];
    loading = false;
    error = false;

    constructor(
        private achievementsService: AchievementsService,
        private toastrService: ToastrService,
        private translateService: TranslateService
    ) {}

    ngOnInit(): void {
        this.loading = true;
        this.achievementsService.getUserAchievements().subscribe({
            next: achievements => {
                this.userAchievements = achievements;
                this.loading = false;
            },
            error: error => {
                console.error('Error fetching achievements', error);
                this.toastrService.error(
                    this.translateService.instant(
                        'Failed to load achievements'
                    ),
                    this.translateService.instant('Error')
                );
                this.loading = false;
                this.error = true;
            },
        });
    }
}
