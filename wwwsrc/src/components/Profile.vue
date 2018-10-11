<template>
    <v-dialog v-model="showProfile" fullscreen transition="slide-y-transition">
        <v-btn slot="activator" flat fab>
            <v-icon>fas fa-user</v-icon>
        </v-btn>
        <v-toolbar app color="amber darken-4">
            <v-spacer></v-spacer>
            <v-btn @click="showProfile = !showProfile" fab flat>
                <v-icon>close</v-icon>
            </v-btn>
        </v-toolbar>
        <v-container id="h2" fill-height fluid class="blue-grey lighten-5">
            <v-layout fill-height align-start column>
                <h1 class="mt-5 pa-1">{{user.username}}</h1>
                <v-expansion-panel class="mt-3">
                    <v-expansion-panel-content id="h-exp" class="blue-grey lighten-4 mb-3">
                        <h3 slot="header" class="pa-2">you've authored {{usersKeeps.length}} keeps</h3>
                        <v-layout fill-height align-center wrap>
                            <v-flex xs12 sm12 md2 lg3 pa-1 v-for="keep in usersKeeps" :key="keep.id">
                                <keeps :keep="keep" :fromProfile="true" />
                            </v-flex>
                        </v-layout>
                    </v-expansion-panel-content>
                </v-expansion-panel>
                <h2 class="pa-3 mt-5">your vaults</h2>
                <v-container fill-height>
                    <v-layout justify-start align-start wrap>
                        <v-flex pa-1 v-for="(vault, index) in usersVaults" :key="vault.id" style="max-width:fit-content;">
                            <vaults :vault="vault" :i="index + 3" :user="user" />
                        </v-flex>
                        <!-- this is the create a vault square -->
                        <v-flex pa-1 mb-3>
                            <v-card color="amber darken-4" width="20vw" height="10vw">
                                <v-layout v-if="!createVault" fill-height column align-center justify-center>
                                    <h1>Create a Vault</h1>
                                    <v-btn @click="createVault = !createVault" icon color="blue-grey lighten-4" small>
                                        <v-icon large>fas fa-plus-circle</v-icon>
                                    </v-btn>
                                </v-layout>
                                <v-layout v-else fill-height column align-center justify-center>
                                    <form>
                                        <v-card v-if="!showDescription" raised color="orange" class="pl-2 pr-2 mt-2">
                                            <v-text-field v-if="newVault.name.length < 1" color="cyan lighten-3" single-line v-model="newVault.name" label="name"></v-text-field>                                    
                                            <v-text-field v-else append-icon="fas fa-forward" @click:append="showDescription = !showDescription" color="cyan lighten-3" single-line v-model="newVault.name"></v-text-field>                                    
                                        </v-card>
                                        <v-card v-else raised color="orange" class="pl-2 pr-2 mt-2">
                                            <v-text-field v-if="newVault.description.length < 1" prepend-icon="fas fa-backward" @click:prepend="showDescription = !showDescription" color="cyan lighten-3" single-line v-model="newVault.description" label="description"></v-text-field>                                    
                                            <v-text-field v-else append-icon="fas fa-share-square" @click:append="submitVault()" prepend-icon="fas fa-backward" @click:prepend="showDescription = !showDescription" color="cyan lighten-3" single-line v-model="newVault.description"></v-text-field>                                    
                                        </v-card>
                                    </form>
                                    <div>
                                        <v-btn @click="createVault = !createVault" fab small color="orange darken-3">
                                            <v-icon>close</v-icon>
                                        </v-btn>
                                    </div>
                                </v-layout>
                            </v-card>
                        </v-flex>
                    </v-layout>
                </v-container>
            </v-layout>
        </v-container>
    </v-dialog>
</template>
<script>
import Keeps from "@/components/Keeps";
import Vaults from "@/components/Vaults";
export default {
  name: "profile",
  data() {
    return {
      showProfile: false,
      createVault: false,
      showDescription: false,
      newVault: {
        name: "",
        description: ""
      }
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    usersKeeps() {
      return this.$store.state.usersKeeps;
    },
    usersVaults() {
      return this.$store.state.usersVaults;
    }
  },
  components: {
    Keeps,
    Vaults
  },
  methods: {
    submitVault() {
      let v = this.newVault;
      v.userId = this.user.id;
      this.$store.dispatch("createVault", v);
      this.createVault = false;
      this.showDescription = false;
      this.newVault = {
        name: "",
        description: ""
      };
    }
  }
};
</script>
<style>
#h2 {
  overflow-y: scroll;
}
#h-exp {
  height: fit-content;
  z-index: 1;
}
</style>