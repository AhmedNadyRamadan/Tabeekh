import { AsyncPipe } from '@angular/common';
import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PaginatorModule, PaginatorState } from 'primeng/paginator';
import { Observable, of } from 'rxjs';
import IChiefDefault from '../../../core/models/IChief.model';
import { IPaginationData } from '../../../core/models/IPagination.model';
import { ChiefCardComponent } from '../../../shared/components/chief-card/chief-card.component';
import IChiefsQuery from './models/IChiefsQuery.model';
import { chiefsService } from './services/chiefs/chiefs.service';

@Component({
  selector: 'app-chiefs',
  imports: [AsyncPipe, ChiefCardComponent, PaginatorModule, FormsModule],
  templateUrl: './chiefs.component.html',
  styleUrl: './chiefs.component.scss',
})
export class ChiefsComponent implements OnInit {
  data: Observable<IPaginationData<IChiefDefault>> = of({
    items: [],
    totalCount: 0,
  });

  queryParams: WritableSignal<IChiefsQuery> = signal<IChiefsQuery>(
    this.defaultQuery
  );

  constructor(private _chiefsService: chiefsService) {}

  ngOnInit(): void {
    this.getChiefs();
  }

  onPageChange(event: PaginatorState) {
    this.queryParams.update((value) => {
      return { ...value, limit: event.rows!, pageNumber: event.page! + 1 };
    });
    this.getChiefs();
  }

  getChiefs() {
    this.data = this._chiefsService.getAll(this.queryParams());
    this.data.subscribe((res) => console.log(res));
  }

  reset() {
    window.location.reload();
    this.queryParams.set(this.defaultQuery);
    this.data = this._chiefsService.getAll(this.queryParams());
  }

  get isDisabled() {
    return this.queryParams().name!.length === 0;
  }

  get first() {
    return 0;
  }

  // get chiefs() {
  //   return this.data.pipe(map((value) => value.items));
  // }

  // get totalCount() {
  //   return this.data.pipe(map((value) => value.totalCount));
  // }

  get defaultQuery() {
    return {
      name: '',
      category: '',
      pageNumber: 1,
      limit: 10,
    };
  }
}
