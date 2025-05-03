import EUserMode from '../enums/EUserMode.enum';
import GUID from '../types/GUID.type';

export interface ILoginCredentials {
  email: string;
  password: string;
}

export interface IUserSecretData extends ILoginCredentials {
  phone: string;
  address: string;
  role: EUserMode;
}

export default interface IUserDefault {
  id: GUID;
  name: string;
  photo: string;
}

export interface IUser extends IUserDefault, IUserSecretData {}
export interface IUserDisplay extends Omit<IUser, 'password'> {}
export interface IUserUpdate extends Omit<IUserDisplay, 'name'> {
  username: IUserDisplay['name'];
}
