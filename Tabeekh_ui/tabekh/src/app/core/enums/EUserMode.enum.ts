enum EUserMode {
    Chief = 0,
    Customer = 1,
    Delivery = 2,
    Admin = 4,
    General = -1
}

export enum EUserModeAr{
    Chief = "شيف",
    Customer = "عميل",
    Delivery = "مسؤول توصيل",
    Admin = "مدير النظام",
    General = "لا أحد"
}

export default EUserMode;