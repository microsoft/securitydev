import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Moment } from 'moment';
// models
import { AlertFilterData } from '../../../../models/response';
// services
import { AlertFilterService } from '../../../../services';

@Component({
    selector: 'app-filters',
    templateUrl: './filters.component.html',
    styleUrls: ['./filters.component.css']
})
export class FiltersComponent implements OnInit {
    public filtersForm: FormGroup;
    public filterData: AlertFilterData;
    @Output() submitForm: EventEmitter<void>;

    constructor(private alertFilterService: AlertFilterService) {
        this.submitForm = new EventEmitter<void>();
    }

    // for date time validation
    public min: Moment;
    public max: Moment;

    ngOnInit(): void {
        this.filtersForm = new FormGroup({
            // alert filters
            'top': new FormControl(),
            'alert:category': new FormControl(),
            'alert:severity': new FormControl(),
            'alert:status': new FormControl(),
            'alert:feedback': new FormControl(),
            'alert:title': new FormControl(),
            'alert:startdatetime': new FormControl(),
            'alert:enddatetime': new FormControl(),
            // vendor filters
            'vendor:vendor': new FormControl(),
            'vendor:provider': new FormControl(),
            'vendor:subprovider': new FormControl(),
            'vendor:version': new FormControl(),
            // user state filters
            'user:aaduserid': new FormControl(),
            'user:accountname': new FormControl(),
            'user:domainname': new FormControl(),
            'user:emailrole': new FormControl(),
            'user:isvpn': new FormControl(),
            'user:logondatetime': new FormControl(),
            'user:logonid': new FormControl(),
            'user:logonip': new FormControl(),
            'user:logonlocation': new FormControl(),
            'user:logontype': new FormControl(),
            'user:onpremisessecurityidentifier': new FormControl(),
            'user:riskscore': new FormControl(),
            'user:accounttype': new FormControl(),
            'user:upn': new FormControl(),
            // host state filters
            'host:fqdn': new FormControl(),
            'host:isazureadjoined': new FormControl(),
            'host:isazureadregistered': new FormControl(),
            'host:ishybridazuredomainjoined': new FormControl(),
            'host:netbiosname': new FormControl(),
            'host:os': new FormControl(),
            'host:privateipaddress': new FormControl(),
            'host:publicipaddress': new FormControl(),
            'host:riskscore': new FormControl(),
            // trigger filters
            'trigger:name': new FormControl(),
            'trigger:value': new FormControl(),
            'trigger:type': new FormControl(),
            // network connections
            'netconn:applicationname': new FormControl(),
            'netconn:destinationaddress': new FormControl(),
            'netconn:destinationdomain': new FormControl(),
            'netconn:destinationport': new FormControl(),
            'netconn:destinationurl': new FormControl(),
            'netconn:direction': new FormControl(),
            'netconn:domainregistereddatetime': new FormControl(),
            'netconn:localdnsname': new FormControl(),
            'netconn:natdestinationaddress': new FormControl(),
            'netconn:natdestinationport': new FormControl(),
            'netconn:natsourceaddress': new FormControl(),
            'netconn:natsourceport': new FormControl(),
            'netconn:protocol': new FormControl(),
            'netconn:riskscore': new FormControl(),
            'netconn:sourceaddress': new FormControl(),
            'netconn:sourceport': new FormControl(),
            'netconn:status': new FormControl(),
            'netconn:urlparameters': new FormControl(),
            // file state filters
            'file:name': new FormControl(),
            'file:path': new FormControl(),
            'file:riskscore': new FormControl(),
            // process filters
            'process:accountname': new FormControl(),
            'process:commandline': new FormControl(),
            'process:createddatetime': new FormControl(),
            'process:integritylevel': new FormControl(),
            'process:iselevated': new FormControl(),
            'process:name': new FormControl(),
            'process:parentprocesscreateddatetime': new FormControl(),
            'process:parentprocessid': new FormControl(),
            'process:parentprocessname': new FormControl(),
            'process:path': new FormControl(),
            'process:processid': new FormControl(),
            // key update filters
            'regkeyupdate:hive': new FormControl(),
            'regkeyupdate:valuetype': new FormControl(),
            'regkeyupdate:operation': new FormControl(),
            'regkeyupdate:oldkey': new FormControl(),
            'regkeyupdate:oldvaluename': new FormControl(),
            'regkeyupdate:oldvaluedata': new FormControl(),
            'regkeyupdate:key': new FormControl(),
            'regkeyupdate:valuename': new FormControl(),
            'regkeyupdate:valuedata': new FormControl(),
            'regkeyupdate:processid': new FormControl(),
            // malware filters
            'malware:name': new FormControl(),
            'malware:category': new FormControl(),
            'malware:family': new FormControl(),
            'malware:severity': new FormControl(),
            'malware:wasrunning': new FormControl(),
            // tag filters
            'tag:title': new FormControl(),
            // vulnerability state filters
            'vulnerability:cve': new FormControl(),
            'vulnerability:severity': new FormControl(),
            'vulnerability:wasrunning': new FormControl(),
            // cloud app state filters
            'cloudapp:destinationservicename': new FormControl(),
            'cloudapp:destinationserviceip': new FormControl(),
            'cloudapp:riskscore': new FormControl(),
            // detection filters
            'detection:id': new FormControl(),
        });

        this.initializeForm();

        this.filtersForm.get('alert:startdatetime').valueChanges.subscribe(val => this.min = val);
        this.filtersForm.get('alert:enddatetime').valueChanges.subscribe(val => this.max = val);
    }

    private initializeForm(): void {
        // get data for filters
        this.filterData = this.alertFilterService.AlertFilters;
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
        this.alertFilterService.AlertFilters = this.filtersForm.value;
        // emit event
        this.submitForm.emit();
    }

    public clearFilters(): void {
        // reset filters
        this.alertFilterService.ResetFilters();
        // update form
        this.initializeForm();
    }
}
