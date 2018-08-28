
import { HubConnection } from "@aspnet/signalr";

declare module "vue/types/vue" {

    interface Vue {
        $signalR: { [index: string]: HubConnection },
        $ListenFor: <T>(hubName: string, topic: string, handler: (data: T) => void) => void
    }
}
