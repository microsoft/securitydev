import { ControlBase, BaseControlOptions, ControlType, DropdownFilterOptions } from '.';

export class DropdownControl<T> extends ControlBase<T> {
    controlType = ControlType.DropDown;
    options: T[] = [];

    constructor(options: DropdownFilterOptions<T> = {}) {
        super(options);
        this.options = options.options || [];
    }
}

export interface DropdownFilterOptions<T> extends BaseControlOptions<T> {
    options?: T[];
}
