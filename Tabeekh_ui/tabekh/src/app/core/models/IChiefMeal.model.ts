import IChiefDefault from "./IChief.model";
import { IMealDefault } from "./IMeal.model";

export default interface IChiefMeals extends IChiefDefault{
    meals: IMealDefault[]
}