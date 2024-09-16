import { PostWhereUniqueInput } from "../post/PostWhereUniqueInput";

export type CommentCreateInput = {
  author?: string | null;
  content?: string | null;
  post?: PostWhereUniqueInput | null;
};
