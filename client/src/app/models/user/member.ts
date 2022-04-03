import { userPhoto } from "./userPhoto";

export class Member {
    guId: string;
    userName: string;
    email: string;
    phone: string;
    firstName: string;
    lastName: string;
    profilePhotoUrl: string;
    coverPhotoUrls: string;
    sex: number;
    gender: number;
    isActive: boolean;
    info: string;
    city: string;
    country: string;
    createDateTime: string;
    age: number;
    lastActivity: string;
    dateOfBirth: Date;
    theUserPhotos: userPhoto[];
}

