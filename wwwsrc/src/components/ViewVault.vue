<template>
    <v-dialog v-model="openVault" fullscreen transition="scale-transition">
        <v-toolbar app :color="!(i % 3) ? 'light-blue darken-3' : !(i % 2) ? 'cyan darken-3' : 'teal darken-3'">
            <h2 class="white--text">{{vault.name.toUpperCase()}} has {{vaultsKeeps.length}} keeps</h2>
            <v-spacer></v-spacer>
            <v-subheader>{{vault.description}}</v-subheader>
            <v-spacer></v-spacer>
            <v-btn @click="openVault = !openVault" fab flat>
                <v-icon>close</v-icon>
            </v-btn>
        </v-toolbar>
        <v-container id="h3" fill-height fluid :class="!(i % 3) ? 'light-blue darken-1' : !(i % 2) ? 'cyan darken-1' : 'teal darken-1'">
            <v-layout v-if="!vaultsKeeps.length" justify-center align-center mt-5>
                <v-card width="400px" height="450px" class="transparent">
                    <v-img src="https://github.com/porterwilcox/keepr/blob/master/wwwsrc/src/assets/empty.jpg?raw=true"></v-img>
                    <v-layout justify-center>
                        <v-btn @click="openVault = !openVault" round flat>Start browsing now </v-btn>
                    </v-layout>
                </v-card>
            </v-layout>
            <v-layout v-else justify-start align-center wrap mt-5 pa-5>
                <v-flex pa-1 md4 v-for="keep in vaultsKeeps" :key="keep.id">
                    <keeps :keep="keep" :fromVault="true" :vaultId="vault.id" />
                </v-flex>
            </v-layout>
        </v-container>
    </v-dialog>
</template>
<script>
import Keeps from '@/components/Keeps'
export default {
  name: "viewVault",
  props: ["open", "vault", "i"],
  data() {
    return {
      openVault: false
    };
  },
  watch: {
    open(value) {
      this.openVault = true;
      this.$store.dispatch("getVaultsKeeps", this.vault.id)
    }
  },
  computed: {
      vaultsKeeps() {
          return this.$store.state.vaultsKeeps
      }
  },
  components: {
      Keeps
  }
};
</script>
<style>
#h3 {
    overflow-y: scroll;
}
</style>
