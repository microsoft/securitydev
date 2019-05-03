import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'displayName' })
export class DisplayNamePipe implements PipeTransform {
    transform(value: string): string {
        if (value && typeof value === 'string' && value.indexOf('@') > -1) {
            return value.split('@')[0];
        }
        return value;
    }
}
