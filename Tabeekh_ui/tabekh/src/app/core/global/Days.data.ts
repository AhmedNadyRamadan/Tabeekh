import EDay, { EDayAR } from "../enums/EDay.enum";

type TDays = (keyof typeof EDay);

type TDayObject = {
    id: EDay;
    day: EDayAR;
};

const DaysAR: TDayObject[] = Object.keys(EDay)
    .filter(key => isNaN(Number(key)))
    .map(key => ({
        id: EDay[key as TDays],
        day: EDayAR[key as TDays],
    }));

export const DaysARArr: EDayAR[] = [EDayAR.Sunday, EDayAR.Monday, EDayAR.Tuesday, EDayAR.Wednesday, EDayAR.Thursday, EDayAR.Friday, EDayAR.Saturday];

export default DaysAR;