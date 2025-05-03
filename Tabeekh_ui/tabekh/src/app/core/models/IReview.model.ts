import GUID from '../types/GUID.type';
import IMeal from './IMeal.model';
import { IUser } from './IUser.model';

export default interface IReview {
  id: GUID;
  chief_Id?: IUser['id'];
  meal_id?: IMeal['id'];
  customer_Id: IUser['id'];
  customer_Name: IUser['name'];
  totalRate: number;
  comment: string;
}
export interface IReviewPost
  extends Pick<IReview, 'customer_Id' | 'comment' | 'totalRate'> {}
