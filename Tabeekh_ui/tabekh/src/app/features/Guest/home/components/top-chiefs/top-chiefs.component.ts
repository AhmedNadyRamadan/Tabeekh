import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { CarouselModule } from 'primeng/carousel';
import { responsiveOptions } from '../../../../../core/configs';
import { ChiefCardComponent } from '../../../../../shared/components/chief-card/chief-card.component';
import IChiefDefault from '../../../../../core/models/IChief.model';
import { LookupsService } from '../../../../../core/services/lookups/lookups.service';

@Component({
  selector: 'app-top-chiefs',
  imports: [AsyncPipe, ChiefCardComponent, CarouselModule],
  templateUrl: './top-chiefs.component.html',
  styleUrl: './top-chiefs.component.scss'
})
export class TopchiefsComponent {
  topchiefs!: Observable<IChiefDefault[]>;

  responsiveOptions!: any[];

  constructor(private _LookupsService: LookupsService) { }

  ngOnInit(): void {
    this.responsiveOptions = responsiveOptions;
    this.topchiefs = this._LookupsService.getTopchiefs();
  }
}
