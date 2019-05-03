import { AverageComparativeScore } from './average-comparative-score.model';

export class AlertStatistics {
    secureScore: SecureScore;
    alertsByStatus: ActiveAlert[];
    alertsByEntity: AlertByEntity;
    alertsByProvider: ModelWithTheMostAlert[];
}

export class SecureScore {
    current: number;
    max: number;
    values: AverageComparativeScore[]; // Value[] - old case
}

export class AlertByEntity {
    usersWithTheMostAlerts: ModelWithTheMostAlert[];
    hostsWithTheMostAlerts: ModelWithTheMostAlert[];
    domainsWithTheMostAlerts: ModelWithTheMostAlert[];
    ipWithTheMostAlerts: ModelWithTheMostAlert[];
}

export class ModelWithTheMostAlert {
    specification: Specification;
    values: Value[];
}

export class Specification {
    title: string;
    filterValue: string;
}

export class ActiveAlert {
    statusName: string;
    values: Value[];
}

export class Value {
    name: string;
    amount: string;
}