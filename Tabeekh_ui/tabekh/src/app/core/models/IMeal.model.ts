import EDays from "../enums/EDay.enum";
import EUnit from "../enums/EUnit.enum";
import GUID from "../types/GUID.type";

export interface IMealMini{
    id: GUID,
    name: string;
    totalRate: number;
    category: string;
    price: number;
    oldPrice?: number;
    photo: string;
    available: boolean;
    measure_unit: EUnit;
    chief_name?: string;
}

export interface IMealDefault extends IMealMini{
    chief_id?: number;
    day: EDays[] | EDays;
}

export default interface IMeal extends IMealDefault {
    prepration_Time: number,
    ingredients: string | string[],
    recipe: string | string[],
}

export interface IMealWithArr extends IMeal{
    ingredients: string[], 
    recipe: string[]
}