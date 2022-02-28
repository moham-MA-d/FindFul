import { Photo } from "../photo/photo";

export class Member {
    guId: string;
    userName: string;
    email: string;
    phone: string;
    firstName: string;
    lastName: string;
    photoUrl: string;
    sex: number;
    gender: number;
    isActive: boolean;
    info: string;
    city: string;
    country: string;
    createDateTime: string;
    age: number;
    lastActivity: string;
    photos: Photo[];
}