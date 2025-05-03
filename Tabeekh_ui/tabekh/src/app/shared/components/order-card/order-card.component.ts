
import { Component, input, OnInit, output } from '@angular/core';
import { PanelAfterToggleEvent, PanelBeforeToggleEvent, PanelModule } from 'primeng/panel';
// import { AvatarModule } from 'primeng/avatar'; //AvatarModule,
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import IOrder from '../../../core/models/IOrder.model';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { CartitemComponent } from '../cartitem/cartitem.component';
import { ICartItem } from '../../../core/models';

@Component({
    selector: 'app-order-card',
    imports: [CurrencyPipe, PanelModule, ButtonModule, MenuModule, DatePipe, CartitemComponent],
    templateUrl: './order-card.component.html',
    styleUrl: './order-card.component.scss'
})
export class OrderCardComponent implements OnInit {
    order = input.required<IOrder>();
    items: { label?: string; icon?: string; separator?: boolean }[] = [];
    toggleable = input<boolean>(false);
    settingEnabled = input<boolean>(false);
    onCollapsedChange = output<boolean>();
    onAfterToggle = output<PanelAfterToggleEvent>();
    onBeforeToggle = output<PanelBeforeToggleEvent>();

    get toggleEnabled(){
        return this.toggleable() && this.order().items.length > 0;
    }

    ngOnInit() {
        this.items = [
            {
                label: 'Refresh',
                icon: 'pi pi-refresh'
            },
            {
                label: 'Search',
                icon: 'pi pi-search'
            },
            {
                separator: true
            },
            {
                label: 'Delete',
                icon: 'pi pi-times'
            }
        ];
    }

    collapsedChange(value: boolean){
        this.onCollapsedChange.emit(value);
    }

    afterToggle(event: PanelAfterToggleEvent){
        this.onAfterToggle.emit(event);
    }

    beforeToggle(event: PanelBeforeToggleEvent){
        this.onBeforeToggle.emit(event);
    }

    castCartItem(value: IOrder['items'][0]){
        return value as unknown as ICartItem;
    }

    
}
