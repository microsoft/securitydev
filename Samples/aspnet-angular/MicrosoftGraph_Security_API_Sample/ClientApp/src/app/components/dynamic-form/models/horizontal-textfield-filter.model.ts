import { ControlBase, ControlType, BaseControlOptions } from '.';

export class HorizontalTextfieldControl extends ControlBase<string> {
    controlType = ControlType.HorizontalTextField;
    type: string;

    constructor(options: BaseControlOptions<string> = {}) {
        super(options);
        this.type = options['type'] || '';
    }
}
