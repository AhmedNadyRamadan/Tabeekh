import { IMealMini } from "../../../../core/models";

export default interface IMealFilter extends
    Pick<IMealMini, "name">,
    Pick<IMealMini, "category"> { }