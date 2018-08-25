<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
        <v-card flat color="transparent">
            <v-card-title>
                <h3>
                    Facial Algorithm Configuration (Live)
                </h3>
            </v-card-title>
    
            <v-card-text class="pt-0">
            <v-slider
                v-model="sensitivity"
                :rules="sensitivityRules"
                label="Sensitivity"
                max="10"
                thumb-label="always"
                ticks
            ></v-slider>
            </v-card-text>

            <v-card-text class="pt-0">
            <v-slider
                v-model="size"
                :rules="sizeRules"
                label="Face Size (distance)"
                thumb-label="always"
                ticks
            ></v-slider>
            </v-card-text>

        </v-card>
    </v-slide-y-transition>
  </v-container>
</template>

<script lang="ts">
import Component from 'vue-class-component';
import Vue from 'vue';

@Component({
  props: {
    message: String
  }
})
export default class LiveConfigAlgorithm extends Vue
{
    public size: number = 30;
    public sensitivity: number = 5

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

    public created()
    {
      let conn = this.$signalR["testHub"];
      conn.on("Listen", (message: string) => {
        console.log(message);
      });

      Promise.all([conn.start()])
            .catch(error => console.error(error));
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
