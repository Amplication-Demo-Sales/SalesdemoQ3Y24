/*
------------------------------------------------------------------------------ 
This code was generated by Amplication. 
 
Changes to this file will be lost if the code is regenerated. 

There are other ways to to customize your code, see this doc to learn more
https://docs.amplication.com/how-to/custom-code

------------------------------------------------------------------------------
  */
import * as graphql from "@nestjs/graphql";
import { GraphQLError } from "graphql";
import { isRecordNotFoundError } from "../../prisma.util";
import { MetaQueryPayload } from "../../util/MetaQueryPayload";
import { Guest } from "./Guest";
import { GuestCountArgs } from "./GuestCountArgs";
import { GuestFindManyArgs } from "./GuestFindManyArgs";
import { GuestFindUniqueArgs } from "./GuestFindUniqueArgs";
import { CreateGuestArgs } from "./CreateGuestArgs";
import { UpdateGuestArgs } from "./UpdateGuestArgs";
import { DeleteGuestArgs } from "./DeleteGuestArgs";
import { BookingFindManyArgs } from "../../booking/base/BookingFindManyArgs";
import { Booking } from "../../booking/base/Booking";
import { GuestService } from "../guest.service";
@graphql.Resolver(() => Guest)
export class GuestResolverBase {
  constructor(protected readonly service: GuestService) {}

  async _guestsMeta(
    @graphql.Args() args: GuestCountArgs
  ): Promise<MetaQueryPayload> {
    const result = await this.service.count(args);
    return {
      count: result,
    };
  }

  @graphql.Query(() => [Guest])
  async guests(@graphql.Args() args: GuestFindManyArgs): Promise<Guest[]> {
    return this.service.guests(args);
  }

  @graphql.Query(() => Guest, { nullable: true })
  async guest(
    @graphql.Args() args: GuestFindUniqueArgs
  ): Promise<Guest | null> {
    const result = await this.service.guest(args);
    if (result === null) {
      return null;
    }
    return result;
  }

  @graphql.Mutation(() => Guest)
  async createGuest(@graphql.Args() args: CreateGuestArgs): Promise<Guest> {
    return await this.service.createGuest({
      ...args,
      data: args.data,
    });
  }

  @graphql.Mutation(() => Guest)
  async updateGuest(
    @graphql.Args() args: UpdateGuestArgs
  ): Promise<Guest | null> {
    try {
      return await this.service.updateGuest({
        ...args,
        data: args.data,
      });
    } catch (error) {
      if (isRecordNotFoundError(error)) {
        throw new GraphQLError(
          `No resource was found for ${JSON.stringify(args.where)}`
        );
      }
      throw error;
    }
  }

  @graphql.Mutation(() => Guest)
  async deleteGuest(
    @graphql.Args() args: DeleteGuestArgs
  ): Promise<Guest | null> {
    try {
      return await this.service.deleteGuest(args);
    } catch (error) {
      if (isRecordNotFoundError(error)) {
        throw new GraphQLError(
          `No resource was found for ${JSON.stringify(args.where)}`
        );
      }
      throw error;
    }
  }

  @graphql.ResolveField(() => [Booking], { name: "bookings" })
  async findBookings(
    @graphql.Parent() parent: Guest,
    @graphql.Args() args: BookingFindManyArgs
  ): Promise<Booking[]> {
    const results = await this.service.findBookings(parent.id, args);

    if (!results) {
      return [];
    }

    return results;
  }
}
