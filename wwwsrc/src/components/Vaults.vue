<template>
    <v-flex @mouseenter="showDelete(vault.id)" @mouseleave="hideDelete(vault.id)">
        <v-card v-if="!(i % 3)" color="light-blue darken-3" width="20vw" height="10vw">
            <v-toolbar height="0vh" absolute card>
                <v-spacer></v-spacer>
                <v-btn @click="deleteVault(vault.id)" :id="'vault'+vault.id" icon class="mt-5 buttons">
                    <v-icon>delete</v-icon>
                </v-btn>
            </v-toolbar>
            <v-layout fill-height column align-center justify-space-around>
                <h1 class="white--text">{{vault.name}}</h1>
                <h2>{{vault.description}}</h2>
            </v-layout>
        </v-card>
        <v-card v-else-if="!(i % 2)" color="cyan darken-3" width="20vw" height="10vw">
            <v-toolbar height="0vh" absolute card>
                <v-spacer></v-spacer>
                <v-btn @click="deleteVault(vault.id)" :id="'vault'+vault.id" icon class="mt-5 buttons">
                    <v-icon>delete</v-icon>
                </v-btn>
            </v-toolbar>
            <v-layout fill-height column align-center justify-space-around>
                <h1 class="white--text">{{vault.name}}</h1>
                <h2>{{vault.description}}</h2>
            </v-layout>        </v-card>
        <v-card v-else color="teal darken-3" width="20vw" height="10vw">
            <v-toolbar height="0vh" absolute card>
                <v-spacer></v-spacer>
                <v-btn @click="deleteVault(vault.id)" :id="'vault'+vault.id" icon class="mt-5 buttons">
                    <v-icon>delete</v-icon>
                </v-btn>
            </v-toolbar>
            <v-layout fill-height column align-center justify-space-around>
                <h1 class="white--text">{{vault.name}}</h1>
                <h2>{{vault.description}}</h2>
            </v-layout>
        </v-card>
    </v-flex>
</template>
<script>
export default {
  name: "vaults",
  props: ["vault", "i", "user"],
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
        }
        this.$store.dispatch("deleteVault", v)
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
