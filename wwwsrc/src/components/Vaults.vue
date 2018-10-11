<template>
    <v-flex @mouseenter="showDelete(vault.id)" @mouseleave="hideDelete(vault.id)">
        <v-card :color="!(i % 3) ? 'light-blue darken-3' : !(i % 2) ? 'cyan darken-3' : 'teal darken-3'" width="20vw" height="10vw">
            <v-toolbar height="0vh" absolute card>
                <v-spacer></v-spacer>
                <v-btn @click="deleteVault(vault.id)" :id="'vault'+vault.id" icon class="mt-5 buttons">
                    <v-icon>delete</v-icon>
                </v-btn>
            </v-toolbar>
            <v-layout fill-height column align-center justify-space-around @click="openVault = !openVault" style="cursor:pointer;">
                <h1 class="white--text">{{vault.name}}</h1>
                <h2>{{vault.description}}</h2>
            </v-layout>
        </v-card>
        <vault-view :open="openVault" :vault="vault" :i="i"/>
    </v-flex>
</template>
<script>
import VaultView from "@/components/ViewVault";
export default {
  name: "vaults",
  props: ["vault", "i", "user"],
  data() {
      return {
          openVault: false
      }
  },
  components: {
    VaultView
  },
  methods: {
    showDelete(id) {
      $(`#vault${id}`).css({ visibility: "visible" });
    },
    hideDelete(id) {
      $(`#vault${id}`).css({ visibility: "hidden" });
    },
    deleteVault(id) {
      let v = {
        id,
        userId: this.vault.userId,
        sendingUserId: this.user.id
      };
      this.$store.dispatch("deleteVault", v);
    }
  }
};
</script>
<style scoped>
.buttons {
  visibility: hidden;
}
@media (hover: none) {
  .buttons {
    visibility: visible;
  }
}
</style>
