import { Likers } from './likers';

export class Post {
    id: number;
    content: string;
    userId: number;
    dateOfCreation: Date;
    userKnownAs: string;
    userPhotoUrl: string;
    likers: Likers[];
}
