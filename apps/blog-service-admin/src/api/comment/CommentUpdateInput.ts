import { PostWhereUniqueInput } from "../post/PostWhereUniqueInput";

export type CommentUpdateInput = {
  author?: string | null;
  content?: string | null;
  post?: PostWhereUniqueInput | null;
};
