import TUserMode from "../enums/EUserMode.enum";
import IUserDefault, { IUserSecretData } from "./IUser.model";

export default interface IChiefDefault extends IUserDefault {
    totalRate: number;
}

export interface IChief extends IChiefDefault, IUserSecretData {
    role: TUserMode.Chief;
}