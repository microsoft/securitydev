export class ControlBase<T> {
    value: T;
    key: string;
    title: string;
    placeholder: string;
    required: boolean;
    order: number;
    controlType: ControlType;

    constructor(options: BaseControlOptions<T> = {}) {
        this.value = options.value;
        this.key = options.key || '';
        this.title = options.title || '';
        this.placeholder = options.placeholder || '';
        this.required = !!options.required;
        this.order = options.order === undefined ? 1 : options.order;
        this.controlType = options.controlType || ControlType.VerticalTextField;
    }
}

export interface BaseControlOptions<T> {
    value?: T;
    key?: string;
    title?: string;
    placeholder?: string;
    required?: boolean;
    order?: number;
    controlType?: ControlType;

}

export enum ControlType {
    HorizontalTextField,
    VerticalTextField,
    DropDown,
    Checkbox,
    CheckboxGroup
}
