import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({ name: 'safeDecodeHtml' })
export class SafeDecodeHtmlPipe implements PipeTransform {

    constructor(private sanitizer: DomSanitizer) { }

    transform(encodedHtml) {
        const textAreaTag = document.createElement('textarea');
        textAreaTag.innerHTML = encodedHtml;
        return this.sanitizer.bypassSecurityTrustHtml(textAreaTag.value);
    }
}
