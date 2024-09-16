import { Injectable } from "@nestjs/common";
import { PrismaService } from "../prisma/prisma.service";
import { GuestServiceBase } from "./base/guest.service.base";

@Injectable()
export class GuestService extends GuestServiceBase {
  constructor(protected readonly prisma: PrismaService) {
    super(prisma);
  }
}
