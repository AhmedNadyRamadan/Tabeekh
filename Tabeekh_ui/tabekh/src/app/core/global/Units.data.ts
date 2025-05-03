import EUnit, { EUnitAR } from "../enums/EUnit.enum";

type TUnits = (keyof typeof EUnit);

type TUnitObject = {
    id: EUnit;
    unit: EUnitAR;
};

const UnitsAR: TUnitObject[] = Object.keys(EUnit)
    .filter(key => isNaN(Number(key)))
    .map(key => ({
        id: EUnit[key as TUnits],
        unit: EUnitAR[key as TUnits],
    }));

export const UnitsARArr: EUnitAR[] = [EUnitAR.Kilogrm, EUnitAR.Piece, EUnitAR.Other];

export default UnitsAR;