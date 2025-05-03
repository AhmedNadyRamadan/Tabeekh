import { AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CarouselModule } from 'primeng/carousel';
import { BehaviorSubject, map, Observable } from 'rxjs';
import IChiefDefault from '../../../../../core/models/IChief.model';
import { IPaginationData } from '../../../../../core/models/IPagination.model';
import { chiefsService } from '../../../chiefs/services/chiefs/chiefs.service';

@Component({
  selector: 'app-banner',
  imports: [AsyncPipe, CarouselModule],
  templateUrl: './banner.component.html',
  styleUrl: './banner.component.scss',
})
export class BannerComponent implements OnInit {
  data!: Observable<IPaginationData<IChiefDefault>>;

  constructor(private _chiefsService: chiefsService) {}

  ngOnInit(): void {
    this.data = this._chiefsService.getAll({ limit: 1000, pageNumber: 1 });
  }

  get chiefs() {
    return this.data
      ? this.data.pipe(map((value) => value.items))
      : new BehaviorSubject([]);
  }
}
