import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Moment } from 'moment';
// models
import { SubscriptionFilterData } from 'src/app/models/response';
// services
import { SubscriptionFilterService } from 'src/app/services';

@Component({
    selector: 'app-subscription-filters',
    templateUrl: './filters.component.html',
    styleUrls: ['./filters.component.css']
})
export class FiltersComponent implements OnInit {
    public filtersForm: FormGroup;
    public filterData: SubscriptionFilterData;
    @Output() submitForm: EventEmitter<void>;

    constructor(private filterService: SubscriptionFilterService) {
        this.submitForm = new EventEmitter<void>();
    }

    // for date time validation
    public min: Moment;
    public max: Moment;

    ngOnInit(): void {
        this.filtersForm = new FormGroup({
            // alert filters
            'subscription:severity': new FormControl(),
            'subscription:status': new FormControl(),
            'subscription:resource': new FormControl(),
            'subscription:expirationstartdatetime': new FormControl(),
            'subscription:expirationenddatetime': new FormControl(),
        });

        this.initializeForm();

        this.filtersForm.get('subscription:expirationstartdatetime').valueChanges.subscribe(val => this.min = val);
        this.filtersForm.get('subscription:expirationenddatetime').valueChanges.subscribe(val => this.max = val);
    }

    private initializeForm(): void {
        // get data for filters
        this.filterData = this.filterService.SubscriptionFilters;
        // get all form controls
        const controls = this.filtersForm.controls;
        // set values to form controls
        for (const key in controls) {
            if (controls.hasOwnProperty(key)) {
                controls[key].setValue(this.filterData[key]);
            }
        }
    }

    public applyFilters(): void {
        // save filters
        this.filterService.SubscriptionFilters = this.filtersForm.value;
        // emit event
        this.submitForm.emit();
    }

    public clearFilters(): void {
        // reset filters
        this.filterService.ResetFilters();
        // update form
        this.initializeForm();
    }
}
