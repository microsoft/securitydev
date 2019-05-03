import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Moment } from 'moment';
// models
import { ActionFilterData } from 'src/app/models/response';
// services
import { ActionFilterService } from 'src/app/services';

@Component({
    selector: 'app-actions-filters',
    templateUrl: './filters.component.html',
    styleUrls: ['./filters.component.css']
})
export class FiltersComponent implements OnInit {
    public filtersForm: FormGroup;
    public filterData: ActionFilterData;
    @Output() submitForm: EventEmitter<void>;

    constructor(private actionFilterService: ActionFilterService) {
        this.submitForm = new EventEmitter<void>();
    }

    // for date time validation
    public min: Moment;
    public max: Moment;

    ngOnInit(): void {
        this.filtersForm = new FormGroup({
            // alert filters
            'action:name': new FormControl(),
            'action:target': new FormControl(),
            'action:status': new FormControl(),
            'action:provider': new FormControl(),
            'action:submittedstartdatetime': new FormControl(),
            'action:submittedenddatetime': new FormControl(),
        });

        this.initializeForm();

        this.filtersForm.get('action:submittedstartdatetime').valueChanges.subscribe(val => this.min = val);
        this.filtersForm.get('action:submittedenddatetime').valueChanges.subscribe(val => this.max = val);
    }

    private initializeForm(): void {
        // get data for filters
        this.filterData = this.actionFilterService.ActionFilters;
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
        this.actionFilterService.ActionFilters = this.filtersForm.value;
        // emit event
        this.submitForm.emit();
    }

    public clearFilters(): void {
        // reset filters
        this.actionFilterService.ResetFilters();
        // update form
        this.initializeForm();
    }
}
