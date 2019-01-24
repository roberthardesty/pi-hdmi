<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
        <v-card flat color="transparent">
            <v-card-title>
                    Facial Algorithm Configuration (Live)
            </v-card-title>
            <v-alert :value="isSpectator" type="warning"> Spectator </v-alert>
            <v-alert :value="isControl" type="info"> Controller </v-alert>
            <v-card color="transparent" flat height="150"></v-card>
            <v-card-text class="pt-0">
            <v-slider
                v-model="sensitivity"
                :rules="sensitivityRules"
                label="Sensitivity"
                max="10"
                thumb-label="always"
                ticks
                :readonly="isSpectator"
            ></v-slider>
            </v-card-text>

            <v-card-text class="pt-0">
            <v-slider
                v-model="size"
                :rules="sizeRules"
                label="Face Size (distance)"
                thumb-label="always"
                ticks
                :readonly="isSpectator"
            ></v-slider>
            </v-card-text>
        </v-card>
    </v-slide-y-transition>
        <v-snackbar
          v-model="faceAlert"
          :timeout="2000"
          :vertical="true"
        >
          {{ faceDetectionStats.IsZeroFaceAlert ? "Face Lost" : "Found a Face!" }}
          <v-btn
            dark
            flat
            @click="faceAlert = false"
          >
            Close
          </v-btn>
        </v-snackbar>
  </v-container>
</template>

<script lang="ts">
import Component from 'vue-class-component';
import Vue from 'vue';
import HubNames from '../HubNames';
import { FaceDetectionStats, ClientGroupStats, SignalRServerResponse, JoinGroupRequest, FaceDetectionConfiguration } from '@/models';


@Component({
  props: {
    message: String
  }
})
export default class LiveConfigAlgorithm extends Vue
{
    public CONTROL_GROUP: string = "CONTROL_GROUP";
    public SPECTATOR_GROUP: string = "SPECTATOR_GROUP";
    public DETECTOR_GROUP: string = "DETECTOR_GROUP";

    public faceDetectionConfiguration: FaceDetectionConfiguration = 
    {
      MinimumFaceHeight: 30,
      MinimumFaceWidth: 30,
      MinimumNeighbors: 5,
      FaceTimeoutInSeconds: 5,
      ScaleFactor: 1.2
    };

    public size: number = 30;
    public sensitivity: number = 5;
    public faceAlert: boolean = false;
    public clientGroupStats: ClientGroupStats = 
    {
      GroupName: "",
      ConnectedClientGroupCounts: {}
    };
    public faceDetectionStats: FaceDetectionStats = 
    {
      IsZeroFaceAlert: false,
      FaceRectangles: []
    };


    public get isSpectator() { return this.clientGroupStats.GroupName == this.SPECTATOR_GROUP };
    public get isControl() { return this.clientGroupStats.GroupName == this.CONTROL_GROUP };
    
    public get sensitivityRules()
    {
        return [
          (v:any) => v <= 8 && v >= 2 || 'This level of sensitivity produces less than ideal results'
        ]
    }

    public get sizeRules()
    {
        return [
          (v:any) => v >= 20 || 'Smaller face sizes result in a slower running algorithm'
        ]
    }

    public updateFaceDetectionStats(stats: FaceDetectionStats)
    {
      this.faceDetectionStats = stats;
      this.faceAlert = true;
    }

    public updateClientGroupStats(stats: ClientGroupStats)
    {
      this.clientGroupStats.ConnectedClientGroupCounts = stats.ConnectedClientGroupCounts;
      if(stats.GroupName.length)
        this.clientGroupStats.GroupName = stats.GroupName
    }

    public async created()
    {
      this.$ListenFor<FaceDetectionStats>(HubNames.FaceDetection, "FaceDetectionStats", this.updateFaceDetectionStats);

      this.$ListenFor<ClientGroupStats>(HubNames.FaceDetection, "ClientGroupStats", this.updateClientGroupStats);

      this.$ListenFor<FaceDetectionConfiguration>(HubNames.FaceDetection, "FaceDetectionConfiguration", (config: FaceDetectionConfiguration) => 
      {
        this.faceDetectionConfiguration = config;
      }); 

      let joinRequest: JoinGroupRequest = 
      {
        IsDetector: false,
        Password: 'Not in use'
      };

      let conn = this.$signalR[HubNames.FaceDetection];

      await conn.start()

      await conn.send("Join", joinRequest)
    }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
