export interface Status {
    Id: number;
    Description: string;
}

export enum StatusEnum {
    Ocupat = 1,
    NumarInexistent = 2,
    Neinteresat = 3,
    Succes = 4,
    NuRaspunde = 5,
    Fax = 6,
    Casuta = 7
}
