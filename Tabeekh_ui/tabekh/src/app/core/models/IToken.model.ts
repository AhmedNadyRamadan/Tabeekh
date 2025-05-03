import { IUser } from './IUser.model';

export default interface ITokenPayload
  extends Pick<IUser, 'id' | 'name' | 'email' | 'role' | 'address'> {
  // id: string;
  // name: string;
  // email: string;
  // role: string;
  exp: number;
  iss: string;
  aud: string;
}
