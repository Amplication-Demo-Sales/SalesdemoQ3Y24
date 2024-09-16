import { Module } from "@nestjs/common";
import { GuestModuleBase } from "./base/guest.module.base";
import { GuestService } from "./guest.service";
import { GuestController } from "./guest.controller";
import { GuestResolver } from "./guest.resolver";

@Module({
  imports: [GuestModuleBase],
  controllers: [GuestController],
  providers: [GuestService, GuestResolver],
  exports: [GuestService],
})
export class GuestModule {}
