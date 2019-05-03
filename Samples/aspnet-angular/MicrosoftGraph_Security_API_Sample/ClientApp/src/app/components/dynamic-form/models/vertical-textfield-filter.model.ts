import { ControlBase, ControlType, BaseControlOptions } from '.';

export class VerticalTextfieldControl extends ControlBase<string> {
    controlType = ControlType.VerticalTextField;
    type: string;

    constructor(options: BaseControlOptions<string> = {}) {
        super(options);
        this.type = options['type'] || '';
    }
}
