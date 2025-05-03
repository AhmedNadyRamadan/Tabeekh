import EUserMode, { EUserModeAr } from "../../../../core/enums/EUserMode.enum";

type TUserModes = (keyof typeof EUserMode);

const DefaultKeys: TUserModes[] = ["Chief", "Customer"];

type TUserModeObject = {
    id: EUserMode;
    role: EUserModeAr;
};

const UserRoles: TUserModeObject[] = Object.keys(EUserMode)
    .filter(key => isNaN(Number(key)) && DefaultKeys.includes(key as TUserModes))
    .map(key => ({
        role: EUserModeAr[key as TUserModes],
        id: EUserMode[key as TUserModes],
    }));

export default UserRoles;