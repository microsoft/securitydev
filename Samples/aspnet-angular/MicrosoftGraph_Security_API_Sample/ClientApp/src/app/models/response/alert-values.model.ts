export class AlertValues {
    constructor(
        public alertStatuses: string[] = [],
        public alertSeverities: string[] = [],
        public alertFeedbacks: string[] = [],
        public alertProviders: string[] = [],
        public alertCategories: string[] = []
    ) { }
}
