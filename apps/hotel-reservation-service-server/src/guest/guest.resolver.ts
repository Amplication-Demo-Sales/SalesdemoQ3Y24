import * as graphql from "@nestjs/graphql";
import { GuestResolverBase } from "./base/guest.resolver.base";
import { Guest } from "./base/Guest";
import { GuestService } from "./guest.service";

@graphql.Resolver(() => Guest)
export class GuestResolver extends GuestResolverBase {
  constructor(protected readonly service: GuestService) {
    super(service);
  }
}
