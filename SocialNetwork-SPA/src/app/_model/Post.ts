import { Likers } from './likers';
import { PostComment } from './PostComment';

export class Post {
    id: number;
    content: string;
    userId: number;
    dateOfCreation: Date;
    userKnownAs: string;
    userPhotoUrl: string;
    likers: Likers[];
    comments: PostComment[];
}
