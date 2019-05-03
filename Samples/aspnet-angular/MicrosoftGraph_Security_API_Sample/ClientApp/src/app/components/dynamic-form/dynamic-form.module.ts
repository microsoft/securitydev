import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// common components
import { CommonComponentsModule } from '../common/common-components.module';
// models
// components
import { DynamicFormControlComponent } from './components/dynamic-form-control/dynamic-form-control.component';
import { DynamicFormComponent } from './components/dynamic-form/dynamic-form.component';

const components = [
    DynamicFormControlComponent,
    DynamicFormComponent
];

@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        CommonComponentsModule,
    ],
    declarations: components,
    exports: components
})

export class DynamicFormModule { }
