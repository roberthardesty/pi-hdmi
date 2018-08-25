<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <live-config-algorithm />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import LiveConfigAlgorithm from '@/components/LiveConfigAlgorithm.vue'; // @ is an alias to /src

@Component({
  components: {
    LiveConfigAlgorithm,
  },
})
export default class Home extends Vue {

}
</script>
